import os

import matplotlib
import numpy as np
from matplotlib import pyplot as plt

import calculating
import plotting
import reading
import time


###############################################################
#                         Positions                           #
###############################################################
def plot_one_ghost():
    data_folder_loc = "data"
    loc = "Dynamic/9a49c0b5-1e25-48c2-8e0f-205456ddc2f6"
    directory = os.path.join(data_folder_loc, loc)

    for root, dirs, files in os.walk(directory):
        for file in files:
            file_loc = os.path.join(data_folder_loc, loc) + "/" + file
            error, transparency, ghost_pointer_pos, student_pointer_pos, statistics = reading.read_file(file_loc)
            plotting.plot_ghost_loc(ghost_pointer_pos)
            break



def plot_positions():
    """
    This method plots the hand pointer positions of all students, and that of the ghost once.
    """
    plot_ghost_instructor = True
    data_folder_loc = "data"
    folder = 0

    for loc in ["Dynamic/9a49c0b5-1e25-48c2-8e0f-205456ddc2f6",
                "Dynamic/740135b4-d405-4344-8a74-ba4150735a52 - DYNAMIC starting said 1 trial late",
                "Dynamic/a482aae3-79df-4a1d-b97e-0389981ebbc5",
                "Dynamic/b9519078-d58e-48e6-ac16-dceae5a99419",
                "Dynamic/e7bb14b2-b84e-4ddd-9211-a91b3db84dc9",
                "Dynamic/1b4eb5be-bfd8-469a-af2e-12c161da71cf",
                "Dynamic/49d98c97-7c00-46b3-9ee1-18b97251c7f9",
                "Dynamic/91429c98-ac03-4c18-9709-fad77174f09e",
                "Dynamic/569818b9-4219-4bfa-bac3-96d990fae211",
                "Dynamic/f5d59c31-e14e-4d75-b9f6-b32c122ecf4a",
                "Static/1f5a0878-18ad-4265-ad2c-f7251cd7e685",
                "Static/74e76fb7-a035-443a-9e33-4fa00cbb148e",
                "Static/138273ef-f48b-4618-b024-f5266055b717",
                "Static/e2969b1c-338e-486e-a06b-c7184a35d568",
                "Static/edf11c76-6b2d-4b1d-860c-e7b5ad4dc80b",
                "Static/865337a1-4455-42d0-8b8e-07355f00c6f5",
                "Static/bc7e5365-3d73-4d7e-a72f-2683fc963688",
                "Static/dcd1af2f-61f7-4ba4-8ec3-d9f684092233",
                "Static/e64da10c-62b4-4553-933e-fbbdbcdb9680",
                "Static/efa98dbd-8c8e-4663-8b19-841477763392"]:
        student_type = "Dynamic" if folder < 10 else "Static"
        directory = os.path.join(data_folder_loc, loc)

        folder += 1

        plot_positions_one_student(plot_ghost_instructor, folder, student_type, directory)
        print(folder)

        if plot_ghost_instructor:
            plot_ghost_instructor = False


def plot_positions_one_student(plot_ghost_instructor, student_num, dynamic, directory):
    """
    This method plots the position graphs for all trials of a student.
    :param plot_ghost_instructor: True if the ghost positions should be graphed too.
    :param student_num: The number associated with the student.
    :param dynamic: This is used for the graph title to label dynamic students as such.
    :param directory: The directory that belongs to the student.
    :return:
    """
    for root, dirs, files in os.walk(directory):
        for file in files:
            trial_index = file[len(file) - 6:len(file) - 4]
            trial_index = int(trial_index[1]) if trial_index[0] == "_" else int(trial_index)
            plot_positions_one_trial(plot_ghost_instructor, student_num, trial_index, dynamic, directory + "/" + file)
            if plot_ghost_instructor:
                plot_ghost_instructor = False


def plot_positions_one_trial(plot_ghost_instructor, student_num, trial_index, dynamic, file_loc):
    """
    This method plots the position graphs for one trial of one student.
    :param plot_ghost_instructor: True if the ghost positions should be graphed too.
    :param student_num: The number associated with the student.
    :param trial_index: The index of the trial.
    :param dynamic: This is used for the graph title to label dynamic students as such.
    :param file_loc: The location of the file that should be read.
    """
    error, transparency, ghost_pointer_pos, student_pointer_pos, statistics = reading.read_file(file_loc)

    if plot_ghost_instructor:
        plotting.plot_ghost_loc(ghost_pointer_pos)

    plotting.plot_student_loc(student_pointer_pos, trial_index, student_num, dynamic)
    time.sleep(2)


def plot_best_positions():
    """
    This method plots the hand pointer positions of all students' first training trial, best training trial, and best test trial based on DTW distance.
    """
    data_folder_loc = "data"
    folder = 0

    for loc in ["Dynamic/9a49c0b5-1e25-48c2-8e0f-205456ddc2f6",
                "Dynamic/740135b4-d405-4344-8a74-ba4150735a52 - DYNAMIC starting said 1 trial late",
                "Dynamic/a482aae3-79df-4a1d-b97e-0389981ebbc5",
                "Dynamic/b9519078-d58e-48e6-ac16-dceae5a99419",
                "Dynamic/e7bb14b2-b84e-4ddd-9211-a91b3db84dc9",
                "Dynamic/1b4eb5be-bfd8-469a-af2e-12c161da71cf",
                "Dynamic/49d98c97-7c00-46b3-9ee1-18b97251c7f9",
                "Dynamic/91429c98-ac03-4c18-9709-fad77174f09e",
                "Dynamic/569818b9-4219-4bfa-bac3-96d990fae211",
                "Dynamic/f5d59c31-e14e-4d75-b9f6-b32c122ecf4a",
                "Static/1f5a0878-18ad-4265-ad2c-f7251cd7e685",
                "Static/74e76fb7-a035-443a-9e33-4fa00cbb148e",
                "Static/138273ef-f48b-4618-b024-f5266055b717",
                "Static/e2969b1c-338e-486e-a06b-c7184a35d568",
                "Static/edf11c76-6b2d-4b1d-860c-e7b5ad4dc80b",
                "Static/865337a1-4455-42d0-8b8e-07355f00c6f5",
                "Static/bc7e5365-3d73-4d7e-a72f-2683fc963688",
                "Static/dcd1af2f-61f7-4ba4-8ec3-d9f684092233",
                "Static/e64da10c-62b4-4553-933e-fbbdbcdb9680",
                "Static/efa98dbd-8c8e-4663-8b19-841477763392"]:
        directory = os.path.join(data_folder_loc, loc)
        folder += 1

        dtw_dist_one_student = [-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1]
        file_locs = [[], [], [], [], [], [], [], [], [], [], [], [], []]

        # Find the best training and best test trials of the student.
        for root, dirs, files in os.walk(directory):
            for file in files:
                trial_index = file[len(file) - 6:len(file) - 4]
                trial_index = int(trial_index[1]) if trial_index[0] == "_" else int(trial_index)

                error, transparency, ghost_pointer_pos, student_pointer_pos, statistics = reading.read_file(
                    directory + "/" + file)

                # Mean-shift the data.
                student_pos = calculating.mean_shift_data(student_pointer_pos)
                ghost_pos = calculating.mean_shift_data(ghost_pointer_pos)

                # Add the DTW distance to the distance arrays.
                dtw_dist = calculating.calculate_dtw_distance(student_pos, ghost_pos)
                dtw_dist_one_student[trial_index] = dtw_dist

                # Store the file location.
                file_locs[trial_index] = directory + "/" + file

        best_training = np.argmin(dtw_dist_one_student[1:10]) + 1
        best_test = np.argmin(dtw_dist_one_student[10:]) + 10

        # Plot the first training, best training, and best test trial of the student in one grid.
        plotting.plot_best_positions_one_student(file_locs[0], file_locs[best_training], file_locs[best_test], folder, "Dynamic" if folder - 1 < 10 else "Static")


###############################################################
#                       Transparency                          #
###############################################################
def plot_transparency():
    doc = 0
    dynamic_transparencies = [[], [], [], [], [], [], [], [], [], [], [], [], []]
    data_folder_loc = "data"

    for loc in ["Dynamic/9a49c0b5-1e25-48c2-8e0f-205456ddc2f6",
                "Dynamic/740135b4-d405-4344-8a74-ba4150735a52 - DYNAMIC starting said 1 trial late",
                "Dynamic/a482aae3-79df-4a1d-b97e-0389981ebbc5",
                "Dynamic/b9519078-d58e-48e6-ac16-dceae5a99419",
                "Dynamic/e7bb14b2-b84e-4ddd-9211-a91b3db84dc9",
                "Dynamic/1b4eb5be-bfd8-469a-af2e-12c161da71cf",
                "Dynamic/49d98c97-7c00-46b3-9ee1-18b97251c7f9",
                "Dynamic/91429c98-ac03-4c18-9709-fad77174f09e",
                "Dynamic/569818b9-4219-4bfa-bac3-96d990fae211",
                "Dynamic/f5d59c31-e14e-4d75-b9f6-b32c122ecf4a",
                "Static/1f5a0878-18ad-4265-ad2c-f7251cd7e685",
                "Static/74e76fb7-a035-443a-9e33-4fa00cbb148e",
                "Static/138273ef-f48b-4618-b024-f5266055b717",
                "Static/e2969b1c-338e-486e-a06b-c7184a35d568",
                "Static/edf11c76-6b2d-4b1d-860c-e7b5ad4dc80b",
                "Static/865337a1-4455-42d0-8b8e-07355f00c6f5",
                "Static/bc7e5365-3d73-4d7e-a72f-2683fc963688",
                "Static/dcd1af2f-61f7-4ba4-8ec3-d9f684092233",
                "Static/e64da10c-62b4-4553-933e-fbbdbcdb9680",
                "Static/efa98dbd-8c8e-4663-8b19-841477763392"]:
        directory = os.path.join(data_folder_loc, loc)
        transparency_list = []
        trials = []
        title = f"Dynamic Student {doc}: Ghost Avatar Transparency over Time"
        if doc > 9:
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
        time.sleep(2)

    plotting.plot_transparency_averages(dynamic_transparencies)

    for i in range(len(dynamic_transparencies)):
        trial_list = dynamic_transparencies[i]
        print(f'Trial: {i + 1}, Min: {min(trial_list)}, Max: {max(trial_list)}')


def plot_completion():
    doc = 0
    data_folder_loc = "data"

    dynamic_completed = [[], [], [], [], [], [], [], [], [], [], [], [], []]
    static_completed = [[], [], [], [], [], [], [], [], [], [], [], [], []]

    for loc in ["Dynamic/9a49c0b5-1e25-48c2-8e0f-205456ddc2f6",
                "Dynamic/740135b4-d405-4344-8a74-ba4150735a52 - DYNAMIC starting said 1 trial late",
                "Dynamic/a482aae3-79df-4a1d-b97e-0389981ebbc5",
                "Dynamic/b9519078-d58e-48e6-ac16-dceae5a99419",
                "Dynamic/e7bb14b2-b84e-4ddd-9211-a91b3db84dc9",
                "Dynamic/1b4eb5be-bfd8-469a-af2e-12c161da71cf",
                "Dynamic/49d98c97-7c00-46b3-9ee1-18b97251c7f9",
                "Dynamic/91429c98-ac03-4c18-9709-fad77174f09e",
                "Dynamic/569818b9-4219-4bfa-bac3-96d990fae211",
                "Dynamic/f5d59c31-e14e-4d75-b9f6-b32c122ecf4a",
                "Static/1f5a0878-18ad-4265-ad2c-f7251cd7e685",
                "Static/74e76fb7-a035-443a-9e33-4fa00cbb148e",
                "Static/138273ef-f48b-4618-b024-f5266055b717",
                "Static/e2969b1c-338e-486e-a06b-c7184a35d568",
                "Static/edf11c76-6b2d-4b1d-860c-e7b5ad4dc80b",
                "Static/865337a1-4455-42d0-8b8e-07355f00c6f5",
                "Static/bc7e5365-3d73-4d7e-a72f-2683fc963688",
                "Static/dcd1af2f-61f7-4ba4-8ec3-d9f684092233",
                "Static/e64da10c-62b4-4553-933e-fbbdbcdb9680",
                "Static/efa98dbd-8c8e-4663-8b19-841477763392"]:
        directory = os.path.join(data_folder_loc, loc)
        for root, dirs, files in os.walk(directory):
            for file in files:
                trial_index = file[len(file) - 6:len(file) - 4]
                trial_index = int(trial_index[1]) if trial_index[0] == "_" else int(trial_index)

                error, transparency, ghost_pointer_pos, student_pointer_pos, statistics = reading.read_file(
                    directory + "/" + file)

                # TTC in Seconds, Completed
                if statistics[5] and doc < 10:
                    dynamic_completed[trial_index].append(statistics[4] / 1000)
                elif statistics[5]:
                    static_completed[trial_index].append(statistics[4] / 1000)
            time.sleep(2)
        doc += 1

    plotting.plot_completion(dynamic_completed, static_completed)


def plot_error():
    doc = 0
    data_folder_loc = "data"
    dynamic_euc_distances = [[], [], [], [], [], [], [], [], [], [], [], [], []]
    dynamic_dtw_distances = [[], [], [], [], [], [], [], [], [], [], [], [], []]
    static_euc_distances = [[], [], [], [], [], [], [], [], [], [], [], [], []]
    static_dtw_distances = [[], [], [], [], [], [], [], [], [], [], [], [], []]

    for loc in ["Dynamic/9a49c0b5-1e25-48c2-8e0f-205456ddc2f6",
                "Dynamic/740135b4-d405-4344-8a74-ba4150735a52 - DYNAMIC starting said 1 trial late",
                "Dynamic/a482aae3-79df-4a1d-b97e-0389981ebbc5",
                "Dynamic/b9519078-d58e-48e6-ac16-dceae5a99419",
                "Dynamic/e7bb14b2-b84e-4ddd-9211-a91b3db84dc9",
                "Dynamic/1b4eb5be-bfd8-469a-af2e-12c161da71cf",
                "Dynamic/49d98c97-7c00-46b3-9ee1-18b97251c7f9",
                "Dynamic/91429c98-ac03-4c18-9709-fad77174f09e",
                "Dynamic/569818b9-4219-4bfa-bac3-96d990fae211",
                "Dynamic/f5d59c31-e14e-4d75-b9f6-b32c122ecf4a",
                "Static/1f5a0878-18ad-4265-ad2c-f7251cd7e685",
                "Static/74e76fb7-a035-443a-9e33-4fa00cbb148e",
                "Static/138273ef-f48b-4618-b024-f5266055b717",
                "Static/e2969b1c-338e-486e-a06b-c7184a35d568",
                "Static/edf11c76-6b2d-4b1d-860c-e7b5ad4dc80b",
                "Static/865337a1-4455-42d0-8b8e-07355f00c6f5",
                "Static/bc7e5365-3d73-4d7e-a72f-2683fc963688",
                "Static/dcd1af2f-61f7-4ba4-8ec3-d9f684092233",
                "Static/e64da10c-62b4-4553-933e-fbbdbcdb9680",
                "Static/efa98dbd-8c8e-4663-8b19-841477763392"]:
        directory = os.path.join(data_folder_loc, loc)

        for root, dirs, files in os.walk(directory):
            euc_dist_one_student = [-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1]
            dtw_dist_one_student = [-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1]
            time_points_one_student = [[], [], [], [], [], [], [], [], [], [], [], [], []]

            for file in files:
                trial_index = file[len(file) - 6:len(file) - 4]
                trial_index = int(trial_index[1]) if trial_index[0] == "_" else int(trial_index)

                error, transparency, ghost_pointer_pos, student_pointer_pos, statistics = reading.read_file(
                    directory + "/" + file)

                # Mean-shift the data.
                time_points_one_student[trial_index] = np.array([t for t, pos in student_pointer_pos]) / 1000
                student_pos = calculating.mean_shift_data(student_pointer_pos)
                ghost_pos = calculating.mean_shift_data(ghost_pointer_pos)

                # Add the Euclidean distance and DTW distance to the distance arrays.
                euc_dist = calculating.calculate_euclidean_distance(student_pos, ghost_pos)
                euc_dist_one_student[trial_index] = euc_dist

                dtw_dist = calculating.calculate_dtw_distance(student_pos, ghost_pos)
                dtw_dist_one_student[trial_index] = dtw_dist

                if doc < 10:
                    dynamic_euc_distances[trial_index].append(np.array(euc_dist).sum())
                    dynamic_dtw_distances[trial_index].append(dtw_dist)
                else:
                    static_euc_distances[trial_index].append(np.array(euc_dist).sum())
                    static_dtw_distances[trial_index].append(dtw_dist)

            time.sleep(2)

            plotting.plot_distance_one_student(
                f"Distance Between the Ghost and {'Dynamic' if doc < 10 else 'Static'} Student {doc + 1}",
                time_points_one_student,
                euc_dist_one_student, dtw_dist_one_student)

        doc += 1

    # Plot the overall distances.
    return plotting.plot_distance_averages(dynamic_euc_distances, dynamic_dtw_distances, static_euc_distances,
                                    static_dtw_distances)


def plot_learning_effect(d_euc, d_dtw, s_euc, s_dtw, std_d_euc, std_d_dtw, std_s_euc, std_s_dtw):
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
    ax1.fill_between(trial_indexes[0:10], d_e_improv + std_d_euc[0:10], d_e_improv - std_d_euc[0:10],
                     facecolor='rebeccapurple', alpha=0.5)
    ax1.plot(trial_indexes, s_e_improv, label="Static", color="cyan")
    ax1.fill_between(trial_indexes[0:10], s_e_improv + std_s_euc[0:10], s_e_improv - std_s_euc[0:10],
                     facecolor='cornflowerblue', alpha=0.5)
    ax1.set_xlabel("Trial")
    ax1.set_ylabel("Distance Improvement")
    ax1.set_xticks(trial_indexes)
    ax1.set_title("Euclidean")
    ax1.grid()

    # Plot the DTW Distance
    ax2.plot(trial_indexes, d_d_improv, label="Dynamic", color="fuchsia")
    ax2.fill_between(trial_indexes[0:10], d_d_improv + std_d_dtw[0:10], d_d_improv - std_d_dtw[0:10],
                     facecolor='rebeccapurple', alpha=0.5)
    ax2.plot(trial_indexes, s_d_improv, label="Static", color="cyan")
    ax2.fill_between(trial_indexes[0:10], s_d_improv + std_s_dtw[0:10], s_d_improv - std_s_dtw[0:10],
                     facecolor='cornflowerblue', alpha=0.5)
    ax2.set_xlabel("Trial")
    ax2.set_ylabel("Distance Improvement")
    ax2.set_xticks(trial_indexes)
    ax1.legend(loc="upper center", bbox_to_anchor=[0.5, -0.15], ncol=2)
    ax2.set_title("Dynamic Time Warping")
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

    print(f'trial 7 -> best test drop dynamic euc: {d_e_7_drop}, '
          f'trial 7 -> best test drop dynamic DTW: {d_d_7_drop}, '
          f'trial 10 -> best test drop dynamic euc: {d_e_drop}, '
          f'trial 10 -> best test drop dynamic DTW: {d_d_drop}, '
          f'trial 10 -> best test drop static euc: {s_e_drop}, '
          f'trial 10 -> best test drop static DTW: {s_d_drop}')


def compare_DTW_automatic_and_manual():
    doc = 0
    data_folder_loc = "data"

    for loc in ["Dynamic/9a49c0b5-1e25-48c2-8e0f-205456ddc2f6",
                "Dynamic/740135b4-d405-4344-8a74-ba4150735a52 - DYNAMIC starting said 1 trial late",
                "Dynamic/a482aae3-79df-4a1d-b97e-0389981ebbc5"]:
        directory = os.path.join(data_folder_loc, loc)

        for root, dirs, files in os.walk(directory):
            euc_dist_one_student = [-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1]
            dtw_dist_one_student = [-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1]
            dtw_dist_manual_one_student = [-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1]
            time_points_one_student = [[], [], [], [], [], [], [], [], [], [], [], [], []]

            for file in files:
                trial_index = file[len(file) - 6:len(file) - 4]
                trial_index = int(trial_index[1]) if trial_index[0] == "_" else int(trial_index)

                error, transparency, ghost_pointer_pos, student_pointer_pos, statistics = reading.read_file(
                    directory + "/" + file)

                # Mean-shift the data.
                time_points_one_student[trial_index] = np.array([t for t, pos in student_pointer_pos]) / 1000
                student_pos = calculating.mean_shift_data(student_pointer_pos)
                ghost_pos = calculating.mean_shift_data(ghost_pointer_pos)

                # Add the Euclidean distance and DTW distance to the distance arrays.
                euc_dist = calculating.calculate_euclidean_distance(student_pos, ghost_pos)
                euc_dist_one_student[trial_index] = euc_dist

                dtw_dist = calculating.calculate_dtw_distance(student_pos, ghost_pos)
                dtw_dist_one_student[trial_index] = dtw_dist
                dtw_dist_manual_one_student[trial_index] = calculating.calculate_dtw_distance_manual(student_pos, ghost_pos)

            time.sleep(2)

            plotting.plot_distance_one_student(
                f"AUTOMATIC Distance Between the Ghost and {'Dynamic' if doc < 10 else 'Static'} Student {doc}",
                time_points_one_student,
                euc_dist_one_student, dtw_dist_one_student)

            plotting.plot_sanity_check(
                f"MANUAL Distance Between the Ghost and {'Dynamic' if doc < 10 else 'Static'} Student {doc}",
                euc_dist_one_student, np.array(dtw_dist_manual_one_student))

            print(dtw_dist_one_student)
            print(np.array(dtw_dist_manual_one_student))
            print(np.array(dtw_dist_one_student) / np.array(dtw_dist_manual_one_student))

        doc += 1


def check_if_any_post_completion_data_is_included():
    doc = 0
    data_folder_loc = "data"

    for loc in ["Dynamic/9a49c0b5-1e25-48c2-8e0f-205456ddc2f6",
                "Dynamic/740135b4-d405-4344-8a74-ba4150735a52 - DYNAMIC starting said 1 trial late",
                "Dynamic/a482aae3-79df-4a1d-b97e-0389981ebbc5",
                "Dynamic/b9519078-d58e-48e6-ac16-dceae5a99419",
                "Dynamic/e7bb14b2-b84e-4ddd-9211-a91b3db84dc9",
                "Dynamic/1b4eb5be-bfd8-469a-af2e-12c161da71cf",
                "Dynamic/49d98c97-7c00-46b3-9ee1-18b97251c7f9",
                "Dynamic/91429c98-ac03-4c18-9709-fad77174f09e",
                "Dynamic/569818b9-4219-4bfa-bac3-96d990fae211",
                "Dynamic/f5d59c31-e14e-4d75-b9f6-b32c122ecf4a",
                "Static/1f5a0878-18ad-4265-ad2c-f7251cd7e685",
                "Static/74e76fb7-a035-443a-9e33-4fa00cbb148e",
                "Static/138273ef-f48b-4618-b024-f5266055b717",
                "Static/e2969b1c-338e-486e-a06b-c7184a35d568",
                "Static/edf11c76-6b2d-4b1d-860c-e7b5ad4dc80b",
                "Static/865337a1-4455-42d0-8b8e-07355f00c6f5",
                "Static/bc7e5365-3d73-4d7e-a72f-2683fc963688",
                "Static/dcd1af2f-61f7-4ba4-8ec3-d9f684092233",
                "Static/e64da10c-62b4-4553-933e-fbbdbcdb9680",
                "Static/efa98dbd-8c8e-4663-8b19-841477763392"]:
        directory = os.path.join(data_folder_loc, loc)
        for root, dirs, files in os.walk(directory):
            for file in files:
                error, transparency, ghost_pointer_pos, student_pointer_pos, statistics = reading.read_file(
                    directory + "/" + file)

                # Get the completion time.
                time_completed = statistics[4]
                # print(time_completed)

                # Check if there were any lines read in with a larger completion time.
                for type in [error, transparency, ghost_pointer_pos, student_pointer_pos]:
                    counter = 0
                    for line in type:
                        if line[0] > time_completed:
                            counter += 1
                            print(line)
                    if counter != 0:
                        print(counter)
            print("Student done")
            time.sleep(2)
        doc += 1


if __name__ == '__main__':
    compare_DTW_automatic_and_manual()
    check_if_any_post_completion_data_is_included()

    plotting.plot_error_transparency_relation()
    plotting.plot_bezier_curve()
    plot_positions()
    plot_one_ghost()
    plot_best_positions()
    plot_transparency()
    plot_completion()

    d_euc, d_dtw, s_euc, s_dtw, std_d_euc, std_d_dtw, std_s_euc, std_s_dtw = plot_error()
    d_euc = np.array(d_euc)
    d_dtw = np.array(d_dtw)
    s_euc = np.array(s_euc)
    s_dtw = np.array(s_dtw)
    std_d_euc = np.array(std_d_euc)
    std_d_dtw = np.array(std_d_dtw)
    std_s_euc = np.array(std_s_euc)
    std_s_dtw = np.array(std_s_dtw)
    plot_learning_effect(d_euc, d_dtw, s_euc, s_dtw, std_d_euc, std_d_dtw, std_s_euc, std_s_dtw)
