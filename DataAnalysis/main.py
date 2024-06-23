import os

import matplotlib
import numpy as np

import calculating
import plotting
import reading
import time
import matplotlib.pyplot as plt

def plot_positions(file_loc, plot_ghost_instructor, student_num, trial_index):
    error, transparency, ghost_pointer_pos, student_pointer_pos, statistics = reading.read_file(file_loc)

    if plot_ghost_instructor:
        plotting.plot_ghost_loc(ghost_pointer_pos)

    plotting.plot_student_loc(student_pointer_pos, trial_index, student_num)


def plot_transparency():
    doc = 0
    dynamic_transparencies = [[], [], [], [], [], [], [], [], [], [], [], [], []]
    data_folder_loc = ""

    for loc in ["Dynamic/9a49c0b5-1e25-48c2-8e0f-205456ddc2f6",
                "Dynamic/740135b4-d405-4344-8a74-ba4150735a52 - DYNAMIC starting said 1 trial late",
                "Dynamic/a482aae3-79df-4a1d-b97e-0389981ebbc5",
                "Dynamic/b9519078-d58e-48e6-ac16-dceae5a99419",
                "Dynamic/e7bb14b2-b84e-4ddd-9211-a91b3db84dc9",
                "Static/1f5a0878-18ad-4265-ad2c-f7251cd7e685",
                "Static/74e76fb7-a035-443a-9e33-4fa00cbb148e",
                "Static/138273ef-f48b-4618-b024-f5266055b717",
                "Static/e2969b1c-338e-486e-a06b-c7184a35d568",
                "Static/edf11c76-6b2d-4b1d-860c-e7b5ad4dc80b"]:
        directory = os.path.join(data_folder_loc, loc)
        transparency_list = []
        trials = []
        title = f"Dynamic Student {doc}: Ghost Avatar Transparency over Time"
        if doc > 4:
            continue
        for root, dirs, files in os.walk(directory):
            for file in files:
                trial_index = file[len(file) - 6:len(file) - 4]
                trial_index = int(trial_index[1]) if trial_index[0] == "_" else int(trial_index)

                error, transparency, ghost_pointer_pos, student_pointer_pos, statistics = reading.read_file(
                    directory + "/" + file)
                for a in transparency:
                    dynamic_transparencies[trial_index].append(a[1])

                if 3 > trial_index or trial_index > 9:
                    continue

                trials.append(trial_index)
                transparency_list.append(transparency)

        plotting.plot_transparency_per_user(transparency_list, title, trials)
        doc += 1

    plotting.plot_transparency_averages(dynamic_transparencies)


def plot_completion():
    doc = 0
    data_folder_loc = ""

    dynamic_completed = [[], [], [], [], [], [], [], [], [], [], [], [], []]
    static_completed = [[], [], [], [], [], [], [], [], [], [], [], [], []]

    for loc in ["Dynamic/9a49c0b5-1e25-48c2-8e0f-205456ddc2f6",
                "Dynamic/740135b4-d405-4344-8a74-ba4150735a52 - DYNAMIC starting said 1 trial late",
                "Dynamic/a482aae3-79df-4a1d-b97e-0389981ebbc5",
                "Dynamic/b9519078-d58e-48e6-ac16-dceae5a99419",
                "Dynamic/e7bb14b2-b84e-4ddd-9211-a91b3db84dc9",
                "Static/1f5a0878-18ad-4265-ad2c-f7251cd7e685",
                "Static/74e76fb7-a035-443a-9e33-4fa00cbb148e",
                "Static/138273ef-f48b-4618-b024-f5266055b717",
                "Static/e2969b1c-338e-486e-a06b-c7184a35d568",
                "Static/edf11c76-6b2d-4b1d-860c-e7b5ad4dc80b"]:
                
        directory = os.path.join(data_folder_loc, loc)
        for root, dirs, files in os.walk(directory):
            for file in files:
                trial_index = file[len(file) - 6:len(file) - 4]
                trial_index = int(trial_index[1]) if trial_index[0] == "_" else int(trial_index)

                error, transparency, ghost_pointer_pos, student_pointer_pos, statistics = reading.read_file(
                    directory + "/" + file)
                # TTC in Seconds, Completed
                if statistics[5] and doc < 5:
                    dynamic_completed[trial_index].append(statistics[4] / 1000)
                elif statistics[5]:
                    static_completed[trial_index].append(statistics[4] / 1000)
            time.sleep(2)
        doc += 1

    plotting.plot_completion(dynamic_completed, static_completed)


def plot_error():
    doc = 0
    data_folder_loc = ""
    dynamic_euc_distances = [[], [], [], [], [], [], [], [], [], [], [], [], []]
    dynamic_dtw_distances = [[], [], [], [], [], [], [], [], [], [], [], [], []]
    static_euc_distances = [[], [], [], [], [], [], [], [], [], [], [], [], []]
    static_dtw_distances = [[], [], [], [], [], [], [], [], [], [], [], [], []]

    for loc in ["Dynamic/9a49c0b5-1e25-48c2-8e0f-205456ddc2f6",
                "Dynamic/740135b4-d405-4344-8a74-ba4150735a52 - DYNAMIC starting said 1 trial late",
                "Dynamic/a482aae3-79df-4a1d-b97e-0389981ebbc5",
                "Dynamic/b9519078-d58e-48e6-ac16-dceae5a99419",
                "Dynamic/e7bb14b2-b84e-4ddd-9211-a91b3db84dc9",
                "Static/1f5a0878-18ad-4265-ad2c-f7251cd7e685",
                "Static/74e76fb7-a035-443a-9e33-4fa00cbb148e",
                "Static/138273ef-f48b-4618-b024-f5266055b717",
                "Static/e2969b1c-338e-486e-a06b-c7184a35d568",
                "Static/edf11c76-6b2d-4b1d-860c-e7b5ad4dc80b"]:
        directory = os.path.join(data_folder_loc, loc)

        for root, dirs, files in os.walk(directory):
            euc_dist_one_student = [-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1]
            dtw_dist_one_student = [-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1]
            time_points_one_student = [[], [], [], [], [], [], [], [], [], [], [], [], []]
            for file in files:
                trial_index = file[len(file) - 6:len(file) - 4]
                trial_index = int(trial_index[1]) if trial_index[0] == "_" else int(trial_index)

                error, transparency, ghost_pointer_pos, student_pointer_pos, statistics = reading.read_file(directory + "/" + file)

                # Mean-shift the data.
                time_points_one_student[trial_index] = np.array([t for t, pos in student_pointer_pos]) / 1000
                student_pos = calculating.mean_shift_data(student_pointer_pos)
                ghost_pos = calculating.mean_shift_data(ghost_pointer_pos)

                # Add the Euclidean distance and DTW distance to the distance arrays.
                euc_dist = calculating.calculate_euclidean_distance(student_pos, ghost_pos)
                euc_dist_one_student[trial_index] = euc_dist

                dtw_dist = calculating.calculate_dtw_distance(student_pos, ghost_pos)
                dtw_dist_one_student[trial_index] = dtw_dist

                if doc < 5:
                    dynamic_euc_distances[trial_index].append(np.array(euc_dist).sum())
                    dynamic_dtw_distances[trial_index].append(dtw_dist)
                else:
                    static_euc_distances[trial_index].append(np.array(euc_dist).sum())
                    static_dtw_distances[trial_index].append(dtw_dist)

            time.sleep(2)

            plotting.plot_distance_one_student(
                f"Distance Between the Ghost and {'Dynamic' if doc < 5 else 'Static'} Student {doc}",
                time_points_one_student,
                euc_dist_one_student, dtw_dist_one_student)

        doc += 1

    # Plot the overall distances.
    plotting.plot_distance_averages(dynamic_euc_distances, dynamic_dtw_distances, static_euc_distances, static_dtw_distances)


if __name__ == '__main__':
    # plot_positions()
    # plot_transparency()
    doc = 0
    # plot_completion()
    # plot_error()

    d_euc = np.array([55.56631225608051, 55.33171889460706, 36.792697239978494, 37.60803537296919, 36.249499118127225,
                      37.71285929640702, 38.697099191943394, 59.971841373152266, 65.71690075192615, 60.867025432784,
                      155.16059847224835, 157.44223801757704, 160.78769558620405])
    std_d_euc = [16.439230091259745, 34.33469973634751, 5.310234034185923, 4.929323466806976, 3.5795609066346343,
                 3.1668606487795765, 5.655700947139746, 3.538099053897585, 6.953726847931437, 6.036407825298509,
                 35.25216483987743, 25.335668754101537, 34.15641377472921]
    d_dtw = np.array([1.4000951772550745, 1.3445630538725386, 0.9097040945606546, 0.9398072837799056, 0.8981152456534893,
                      0.9042874929670681, 0.9159993216639588, 1.5672579594936, 1.697809046445191, 1.5073893636109654,
                      3.4223674058528575, 3.3721161606489938, 3.513590535124223])
    std_d_dtw = [0.40801327794509806, 0.8335517420010515, 0.1321932895279879, 0.13280702848714443, 0.11936085127780494,
                 0.07507333192904148, 0.14669221315095662, 0.3124538869063514, 0.20805270115356195, 0.18708410504724274,
                 0.870427791598894, 0.5795795481248222, 0.7645246640359163]
    s_euc = np.array([75.5571806005723, 41.34693016125898, 37.53671387816295, 36.096627220907735, 35.22180854954307,
                      36.16169437651902, 35.45701385973063, 34.00173709683777, 34.73231472570199, 34.319837852575745,
                      202.18503643697878, 181.15853779353995, 200.8299622761059])
    std_s_euc = [21.608253348301098, 3.431887020710913, 2.356466549512442, 2.691236782087554, 1.956200332520581,
                 3.137099103124101, 3.7575564438197886, 2.3465254543814433, 3.7422140665277404, 2.1787862301321543,
                 53.295339183262755, 20.03554500150295, 48.93781466266533]
    s_dtw = np.array([2.0388258203642686, 1.001118241698458, 0.9295621684235279, 0.8912500472978252, 0.8563376261989225,
                      0.9129958380440254, 0.8676968185616383, 0.8074818570366407, 0.817654799372616, 0.8315065309457316,
                      4.359647635416808, 4.49768887721296, 4.358111628342597])
    std_s_dtw = [0.7033219652814953, 0.11896713724340068, 0.116656820797056, 0.12849535234110462, 0.07283777648404562,
                 0.1086041437727631, 0.11390188361391496, 0.09036172098117862, 0.11588124890217773, 0.07676743221003521,
                 1.3942697259925585, 0.8963799229136475, 1.0865942102518829]

    # Get the improvement arrays. They're based on means though.
    d_e_improv = d_euc[0:10] - d_euc[0]
    d_d_improv = d_dtw[0:10] - d_dtw[0]
    s_e_improv = s_euc[0:10] - s_euc[0]
    s_d_improv = s_dtw[0:10] - s_dtw[0]

    trial_indexes = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
    fig = plt.figure(figsize=plt.figaspect(0.4))
    fig.suptitle("Improvement compared to Trial 1")
    gs = matplotlib.gridspec.GridSpec(1, 2)

    ax1, ax2 = fig.add_subplot(gs[0]), fig.add_subplot(gs[1])
    # Add space between the subplots.
    fig.subplots_adjust(wspace=0.2)

    # Plot the Euclidean Distance
    ax1.plot(trial_indexes, d_e_improv, label="Dynamic", color="fuchsia")
    ax1.fill_between(trial_indexes[0:10], d_e_improv + std_d_euc[0:10], d_e_improv - std_d_euc[0:10], facecolor='rebeccapurple', alpha=0.5)
    ax1.plot(trial_indexes, s_e_improv, label="Static", color="cyan")
    ax1.fill_between(trial_indexes[0:10], s_e_improv + std_s_euc[0:10], s_e_improv - std_s_euc[0:10], facecolor='cornflowerblue', alpha=0.5)
    ax1.set_xlabel("Trial")
    ax1.set_ylabel("Distance Improvement")
    ax1.set_xticks(trial_indexes)
    ax1.set_title("Euclidean")
    ax1.grid()

    # Plot the DTW Distance
    ax2.plot(trial_indexes, d_d_improv, label="Dynamic", color="fuchsia")
    ax2.fill_between(trial_indexes[0:10], d_d_improv + std_d_dtw[0:10], d_d_improv - std_d_dtw[0:10], facecolor='rebeccapurple', alpha=0.5)
    ax2.plot(trial_indexes, s_d_improv, label="Static", color="cyan")
    ax2.fill_between(trial_indexes[0:10], s_d_improv + std_s_dtw[0:10], s_d_improv - std_s_dtw[0:10], facecolor='cornflowerblue', alpha=0.5)
    ax2.set_xlabel("Trial")
    ax2.set_ylabel("Distance Improvement")
    ax2.set_xticks(trial_indexes)
    ax1.legend(loc="upper center", bbox_to_anchor=[0.5, -0.15], ncol=2)
    ax2.grid()

    box = ax1.get_position()
    ax1.set_position([box.x0, box.y0 + box.height * 0.2, box.width, box.height * 0.8])
    box = ax2.get_position()
    ax2.set_position([box.x0, box.y0 + box.height * 0.2, box.width, box.height * 0.8])

    ax1.legend(loc="upper center", bbox_to_anchor=[0.5, -0.15], ncol=2)
    ax2.legend(loc="upper center", bbox_to_anchor=[0.5, -0.15], ncol=2)
    plt.show()
    fig.savefig("Learning_Effect.png")

    # Get the performance drop per array. last training trial - best test trial
    d_e_7_drop = (d_euc[10:].min() - d_euc[6]) / d_euc[6] * 100
    d_d_7_drop = (d_dtw[10:].min() - d_dtw[6]) / d_dtw[6] * 100
    d_e_drop = (d_euc[10:].min() - d_euc[9]) / d_euc[9] * 100
    d_d_drop = (d_dtw[10:].min() - d_dtw[9]) / d_dtw[9] * 100
    s_e_drop = (s_euc[10:].min() - s_euc[9]) / s_euc[9] * 100
    s_d_drop = (s_dtw[10:].min() - s_dtw[9]) / s_dtw[9] * 100

    print(d_e_7_drop, d_d_7_drop, d_e_drop, d_d_drop, s_e_drop, s_d_drop)
